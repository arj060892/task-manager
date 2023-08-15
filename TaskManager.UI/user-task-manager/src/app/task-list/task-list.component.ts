import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { UserTaskResponseDTO } from '../shared/openapi/v1';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteTaskModalComponent } from '../shared/modals/delete-task-modal/delete-task-modal.component';
import { Store } from '@ngrx/store';
import * as TaskActions from '../shared/store/actions/task.actions';
import { Subject, Subscription, takeUntil } from 'rxjs';
import * as fromTasks from '../shared/store/selectors/task.selectors';
import * as moment from 'moment';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Actions, ofType } from '@ngrx/effects';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TaskListComponent implements OnInit {
  todayTasks: UserTaskResponseDTO[] = [];
  upcomingTasks: UserTaskResponseDTO[] = [];
  pastTasks: UserTaskResponseDTO[] = [];
  expandedTaskId: number | null | undefined = null;
  private destroy$ = new Subject<void>();

  constructor(
    private modalService: NgbModal,
    private store: Store,
    private router: Router,
    private toastr: ToastrService,
    private actions$: Actions
  ) {}

  editTask(task: UserTaskResponseDTO): void {
    this.router.navigate(['/task', task.id]);
  }

  deleteTask(task: UserTaskResponseDTO) {
    const modalRef = this.modalService.open(DeleteTaskModalComponent);
    modalRef.componentInstance.task = task;
    modalRef.result.then((taskId) => {
      if (taskId) {
        this.store.dispatch(TaskActions.deleteTask({ taskId: task.id! }));
      }
    });
  }

  ngOnInit(): void {
    this.store.dispatch(TaskActions.loadTasks());
    this.store
      .select(fromTasks.selectAllTasks)
      .pipe(takeUntil(this.destroy$))
      .subscribe((tasks) => {
        const today = moment();
        this.todayTasks = tasks.filter((t) =>
          moment(t.dueDate).isSame(today, 'day')
        );
        this.upcomingTasks = tasks.filter((t) =>
          moment(t.dueDate).isAfter(today, 'day')
        );
        this.pastTasks = tasks.filter((t) =>
          moment(t.dueDate).isBefore(today, 'day')
        );
      });

    this.actions$
      .pipe(ofType(TaskActions.deleteTaskSuccess), takeUntil(this.destroy$))
      .subscribe(() => {
        this.store.dispatch(TaskActions.loadTasks());
        this.toastr.success('Task deleted successfully');
      });

    this.actions$
      .pipe(ofType(TaskActions.deleteTaskFailure), takeUntil(this.destroy$))
      .subscribe((e) => {
        if (e && e.error) {
          this.toastr.error(e.error.error);
        }
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
