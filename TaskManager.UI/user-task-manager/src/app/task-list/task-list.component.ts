import { Component, OnInit } from '@angular/core';
import { UserTaskResponseDTO } from '../shared/openapi/v1';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteTaskModalComponent } from '../shared/modals/delete-task-modal/delete-task-modal.component';
import { Store } from '@ngrx/store';
import * as TaskActions from '../shared/store/actions/task.actions';
@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  // Mock data based on UserTaskResponseDTO
  tasks: UserTaskResponseDTO[] = [
    {
      id: 1,
      title: 'Task 1',
      description: 'Description 1',
      status: 'Open',
      dueDate: new Date().toISOString(),
      startTime: '09:00:00',
      endTime: '17:00:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 2,
      title: 'Task 2',
      description: 'Description 2',
      status: 'Open',
      dueDate: new Date(Date.now() + 24 * 60 * 60 * 1000).toISOString(),
      startTime: '10:00:00',
      endTime: '18:00:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 3,
      title: 'Task 3',
      description: 'Description 3',
      status: 'Open',
      dueDate: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '11:00:00',
      endTime: '19:00:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 4,
      title: 'Task 4',
      description: 'Description 4',
      status: 'Open',
      dueDate: new Date(Date.now() + 3 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '08:00:00',
      endTime: '16:00:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 5,
      title: 'Task 5',
      description: 'Description 5',
      status: 'Open',
      dueDate: new Date(Date.now() + 4 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '12:00:00',
      endTime: '20:00:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 6,
      title: 'Task 6',
      description: 'Description 6',
      status: 'Open',
      dueDate: new Date(Date.now() + 5 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '09:30:00',
      endTime: '17:30:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 7,
      title: 'Task 7',
      description: 'Description 7',
      status: 'Open',
      dueDate: new Date(Date.now() + 6 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '08:30:00',
      endTime: '16:30:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 8,
      title: 'Task 8',
      description: 'Description 8',
      status: 'Open',
      dueDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '10:30:00',
      endTime: '18:30:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 9,
      title: 'Task 9',
      description: 'Description 9',
      status: 'Open',
      dueDate: new Date(Date.now() + 8 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '11:30:00',
      endTime: '19:30:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 10,
      title: 'Task 10',
      description: 'Description 10',
      status: 'Open',
      dueDate: new Date(Date.now() + 9 * 24 * 60 * 60 * 1000).toISOString(),
      startTime: '12:30:00',
      endTime: '20:30:00',
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
  ];

  todayTasks: UserTaskResponseDTO[] = [];
  upcomingTasks: UserTaskResponseDTO[] = [];
  pastTasks: UserTaskResponseDTO[] = [];
  expandedTaskId: number | null | undefined = null;

  constructor(private modalService: NgbModal, private store: Store) {}

  // Handle edit action (to be implemented)
  editTask(task: UserTaskResponseDTO): void {
    // Implement the edit functionality here
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
      (reason) => {
        // Handle dismiss
      }
    );
  }

  ngOnInit(): void {
    this.todayTasks = this.tasks.filter((t) =>
      this.isToday(new Date(t.dueDate!))
    );
    this.upcomingTasks = this.tasks.filter(
      (t) => new Date(t.dueDate!) > new Date()
    );
    this.pastTasks = this.tasks.filter(
      (t) => new Date(t.dueDate!) < new Date()
    );
  }

  isToday(date: Date) {
    const today = new Date();
    return (
      date.getDate() === today.getDate() &&
      date.getMonth() === today.getMonth() &&
      date.getFullYear() === today.getFullYear()
    );
  }
}
