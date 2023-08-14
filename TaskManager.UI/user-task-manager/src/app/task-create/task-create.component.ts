import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserTaskRequestDTO } from '../shared/openapi/v1';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import * as TaskActions from '../shared/store/actions/task.actions';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.scss'],
})
export class TaskCreateComponent implements OnInit {
  taskForm?: FormGroup;
  statuses = ['Pending', 'Completed', 'Removed'];

  constructor(private fb: FormBuilder, private store: Store) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: ['Pending'],
      dueDate: this.getCurrentDate(),
      startTime: this.getCurrentTime(),
      endTime: this.getOneHourLater(),
    });
  }

  onSubmit(): void {
    const taskDTO = this.convertToUserTaskRequestDTO(this.taskForm!.value);
    this.store.dispatch(TaskActions.createTask({ task: taskDTO }));
  }

  getCurrentDate(): NgbDateStruct {
    const today = moment();
    return { year: today.year(), month: today.month() + 1, day: today.date() };
  }

  getCurrentTime(): NgbTimeStruct {
    const now = moment();
    return { hour: now.hours(), minute: now.minutes(), second: now.seconds() };
  }

  getOneHourLater(): NgbTimeStruct {
    const oneHourLater = moment().add(1, 'hours');
    return {
      hour: oneHourLater.hours(),
      minute: oneHourLater.minutes(),
      second: oneHourLater.seconds(),
    };
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
