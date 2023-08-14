import { Component, OnInit } from '@angular/core';
import { UserTaskResponseDTO } from '../shared/openapi/v1';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteTaskModalComponent } from '../shared/modals/delete-task-modal/delete-task-modal.component';
import { Store } from '@ngrx/store';
import * as TaskActions from '../shared/store/actions/task.actions';
import { Subscription } from 'rxjs';
import * as fromTasks from '../shared/store/selectors/task.selectors';
import * as moment from 'moment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  todayTasks: UserTaskResponseDTO[] = [];
  upcomingTasks: UserTaskResponseDTO[] = [];
  pastTasks: UserTaskResponseDTO[] = [];
  expandedTaskId: number | null | undefined = null;
  private tasksSubscription?: Subscription;

  constructor(
    private modalService: NgbModal,
    private store: Store,
    private router: Router
  ) {}

  editTask(task: UserTaskResponseDTO): void {
    this.router.navigate(['/task', task.id]);
  }

  deleteTask(task: UserTaskResponseDTO) {
    const modalRef = this.modalService.open(DeleteTaskModalComponent);
    modalRef.componentInstance.task = task;
    modalRef.result.then(
      (taskId) => {
        if (taskId) {
          console.log(taskId);
          this.store.dispatch(TaskActions.deleteTask({ taskId: task.id! }));
        }
      },
      (reason) => {}
    );
  }

  ngOnInit(): void {
    this.store.dispatch(TaskActions.loadTasks());
    this.tasksSubscription = this.store
      .select(fromTasks.selectAllTasks)
      .subscribe((tasks) => {
        const today = moment();
        this.todayTasks = tasks.filter((t) =>
          moment(t.dueDate).isSame(today, 'day')
        );
        this.upcomingTasks = tasks.filter((t) =>
          moment(t.dueDate).isAfter(today)
        );
        this.pastTasks = tasks.filter((t) => moment(t.dueDate).isBefore(today));
      });
  }

  ngOnDestroy(): void {
    if (this.tasksSubscription) {
      this.tasksSubscription.unsubscribe();
    }
  }
}
