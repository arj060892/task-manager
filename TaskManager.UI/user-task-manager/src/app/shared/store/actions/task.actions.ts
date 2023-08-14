import { createAction, props } from '@ngrx/store';
import { UserTaskResponseDTO } from '../../openapi/v1';

// **READ (or Load)**
// Action to initiate the loading of tasks
export const loadTasks = createAction('[Task] Load Tasks');

// Action for successful task load
export const loadTasksSuccess = createAction(
  '[Task] Load Tasks Success',
  props<{ tasks: UserTaskResponseDTO[] }>()
);

// Action for failed task load
export const loadTasksFailure = createAction(
  '[Task] Load Tasks Failure',
  props<{ error: any }>()
);

// **CREATE**
// Action to initiate the creation of a task
export const createTask = createAction(
  '[Task] Create Task',
  props<{ task: UserTaskResponseDTO }>()
);

// Action for successful task creation
export const createTaskSuccess = createAction(
  '[Task] Create Task Success',
  props<{ task: UserTaskResponseDTO }>()
);

// Action for failed task creation
export const createTaskFailure = createAction(
  '[Task] Create Task Failure',
  props<{ error: any }>()
);

// **UPDATE**
// Action to initiate the update of a task
export const updateTask = createAction(
  '[Task] Update Task',
  props<{ taskId: number; updatedTask: UserTaskResponseDTO }>()
);

// Action for successful task update
export const updateTaskSuccess = createAction(
  '[Task] Update Task Success',
  props<{ updatedTask: UserTaskResponseDTO }>()
);

// Action for failed task update
export const updateTaskFailure = createAction(
  '[Task] Update Task Failure',
  props<{ error: any }>()
);

// **DELETE**
// Action to initiate the deletion of a task
export const deleteTask = createAction(
  '[Task] Delete Task',
  props<{ taskId: number }>()
);

// Action for successful task deletion
export const deleteTaskSuccess = createAction(
  '[Task] Delete Task Success',
  props<{ taskId: number }>()
);

// Action for failed task deletion
export const deleteTaskFailure = createAction(
  '[Task] Delete Task Failure',
  props<{ error: any }>()
);

export const loadTaskById = createAction(
  '[Task] Load Task by ID',
  props<{ taskId: number }>()
);

export const loadTaskByIdSuccess = createAction(
  '[Task] Load Task by ID Success',
  props<{ task: UserTaskResponseDTO }>()
);

export const loadTaskByIdFailure = createAction(
  '[Task] Load Task by ID Failure',
  props<{ error: any }>()
);
