import { Component, OnInit } from '@angular/core';
import { UserTaskResponseDTO } from '../shared/openapi/v1';

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
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    {
      id: 2,
      title: 'Task 2',
      description: 'Description 2',
      status: 'Open',
      dueDate: new Date(Date.now() + 24 * 60 * 60 * 1000).toISOString(),
      createdDate: new Date().toISOString(),
      modifiedDate: new Date().toISOString(),
    },
    // ... add more mock tasks as needed
  ];

  todayTasks: UserTaskResponseDTO[] = [];
  upcomingTasks: UserTaskResponseDTO[] = [];
  pastTasks: UserTaskResponseDTO[] = [];
  expandedTaskId: number | null | undefined = null;

  constructor() {}

  // Handle edit action (to be implemented)
  editTask(task: UserTaskResponseDTO): void {
    // Implement the edit functionality here
  }

  // Handle delete action (to be implemented)
  deleteTask(task: UserTaskResponseDTO): void {
    // Implement the delete functionality here
  }

  ngOnInit(): void {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    this.tasks.forEach((task) => {
      const taskDate = new Date(task.dueDate!); // Assuming dueDate determines the task's date
      taskDate.setHours(0, 0, 0, 0);

      if (taskDate.getTime() === today.getTime()) {
        this.todayTasks.push(task);
      } else if (taskDate.getTime() > today.getTime()) {
        this.upcomingTasks.push(task);
      } else {
        this.pastTasks.push(task);
      }
    });
  }
}
