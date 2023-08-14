import { createReducer, on, Action } from '@ngrx/store';
import { UserTaskResponseDTO } from '../../openapi/v1';
import * as TaskActions from '../actions/task.actions';

export interface TaskState {
  tasks: UserTaskResponseDTO[];
  error: any;
}

export const initialTaskState: TaskState = {
  tasks: [],
  error: null,
};

const _taskReducer = createReducer(
  initialTaskState,

  // **READ (or Load)**
  on(TaskActions.loadTasks, (state) => state),
  on(TaskActions.loadTasksSuccess, (state, { tasks }) => ({ ...state, tasks })),
  on(TaskActions.loadTasksFailure, (state, { error }) => ({ ...state, error })),

  // **CREATE**
  on(TaskActions.createTask, (state) => state),
  on(TaskActions.createTaskSuccess, (state, { task }) => {
    return { ...state, tasks: [...state.tasks, task] };
  }),
  on(TaskActions.createTaskFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // **UPDATE**
  on(TaskActions.updateTask, (state) => state),
  on(TaskActions.updateTaskSuccess, (state, { updatedTask }) => {
    const updatedTasks = state.tasks.map((task) => {
      return task.id === updatedTask.id ? updatedTask : task;
    });
    return { ...state, tasks: updatedTasks };
  }),
  on(TaskActions.updateTaskFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // **DELETE**
  on(TaskActions.deleteTask, (state) => state),
  on(TaskActions.deleteTaskSuccess, (state, { taskId }) => {
    const updatedTasks = state.tasks.filter((task) => task.id !== taskId);
    return { ...state, tasks: updatedTasks };
  }),
  on(TaskActions.deleteTaskFailure, (state, { error }) => ({ ...state, error }))
);

export function taskReducer(state: TaskState | undefined, action: Action) {
  return _taskReducer(state, action);
}
