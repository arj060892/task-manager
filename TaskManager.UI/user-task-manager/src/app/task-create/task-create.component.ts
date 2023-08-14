import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserTaskRequestDTO, UserTaskResponseDTO } from '../shared/openapi/v1';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import * as TaskActions from '../shared/store/actions/task.actions';
import { Store } from '@ngrx/store';
import { ActivatedRoute, Router } from '@angular/router';
import * as fromTasks from '../shared/store/selectors/task.selectors';
import { ToastrService } from 'ngx-toastr';
import { Actions, ofType } from '@ngrx/effects';
import { tap, merge } from 'rxjs';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.scss'],
})
export class TaskCreateComponent implements OnInit {
  taskForm?: FormGroup;
  statuses = ['Pending', 'InProgress', 'Completed'];
  isUpdateMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private actions$: Actions,
    private router: Router
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');
    this.initForm();
    if (taskId) {
      this.isUpdateMode = true;
      this.store.dispatch(TaskActions.loadTaskById({ taskId: +taskId }));
      this.store.select(fromTasks.selectTaskById(+taskId)).subscribe((task) => {
        if (task) {
          this.populateForm(task);
        }
      });
    }

    merge(
      this.actions$.pipe(
        ofType(TaskActions.createTaskSuccess),
        tap(() => {
          this.toastr.success('Task created successfully');
          this.router.navigate(['/tasks']);
        })
      ),
      this.actions$.pipe(
        ofType(TaskActions.createTaskFailure),
        tap((e) => this.toastr.error(e.error.error))
      ),
      this.actions$.pipe(
        ofType(TaskActions.updateTaskSuccess),
        tap(() => {
          this.toastr.success('Task updated successfully');
          this.router.navigate(['/tasks']);
        })
      ),
      this.actions$.pipe(
        ofType(TaskActions.updateTaskFailure),
        tap((e) => this.toastr.error(e.error.error))
      )
    ).subscribe();
  }

  populateForm(task: UserTaskResponseDTO) {
    this.taskForm!.patchValue({
      title: task.title,
      description: task.description,
      status: task.status,
      dueDate: this.toNgbDateStruct(task.dueDate!),
      startTime: this.toNgbTimeStruct(task.startTime!),
      endTime: this.toNgbTimeStruct(task.endTime!),
      id: task.id,
    });
  }

  initForm() {
    const now = moment();
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: ['Pending'],
      dueDate: this.toNgbDateStruct(now.format('YYYY-MM-DD')),
      startTime: this.toNgbTimeStruct(now.format('HH:mm:ss')),
      endTime: this.toNgbTimeStruct(now.add(1, 'hours').format('HH:mm:ss')),
      id: 0,
    });
  }

  private toNgbDateStruct(dateString: string): NgbDateStruct {
    const date = moment(dateString);
    return { year: date.year(), month: date.month() + 1, day: date.date() };
  }

  private toNgbTimeStruct(timeString: string): NgbTimeStruct {
    const time = moment(timeString, 'HH:mm:ss');
    return {
      hour: time.hours(),
      minute: time.minutes(),
      second: time.seconds(),
    };
  }

  onSubmit(): void {
    const taskDTO = this.convertToUserTaskRequestDTO(this.taskForm!.value);
    if (this.isUpdateMode) {
      this.store.dispatch(
        TaskActions.updateTask({
          taskId: this.taskForm?.get('id')?.value as number,
          updatedTask: taskDTO,
        })
      );
    } else {
      this.store.dispatch(TaskActions.createTask({ task: taskDTO }));
    }
  }

  convertToUserTaskRequestDTO(formValue: any): UserTaskRequestDTO {
    const dueDateMoment = moment(
      `${formValue.dueDate.year}-${formValue.dueDate.month}-${formValue.dueDate.day}`
    );
    const startTimeMoment = moment(
      `${formValue.startTime.hour}:${formValue.startTime.minute}:${formValue.startTime.second}`,
      'HH:mm:ss'
    );
    const endTimeMoment = moment(
      `${formValue.endTime.hour}:${formValue.endTime.minute}:${formValue.endTime.second}`,
      'HH:mm:ss'
    );

    const dto: UserTaskRequestDTO = {
      title: formValue.title,
      description: formValue.description,
      status: formValue.status,
      dueDate: dueDateMoment!.format('YYYY-MM-DD'),
      startTime: startTimeMoment.format('HH:mm:ss'),
      endTime: endTimeMoment.format('HH:mm:ss'),
    };

    return dto;
  }
}
