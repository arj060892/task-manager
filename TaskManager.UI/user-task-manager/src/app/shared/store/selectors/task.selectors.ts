import { createFeatureSelector, createSelector } from '@ngrx/store';
import { TaskState } from '../reducers/task.reducer';

// This is a top-level selector that selects the entire task feature slice
const selectTaskState = createFeatureSelector<TaskState>('task');

// This selector retrieves the tasks array
export const selectAllTasks = createSelector(
  selectTaskState,
  (state: TaskState) => state.tasks
);

// This selector retrieves a single task by its ID
export const selectTaskById = (taskId: number) =>
  createSelector(selectAllTasks, (tasks) =>
    tasks.find((task) => task.id === taskId)
  );

// This selector checks for any errors
export const selectTasksError = createSelector(
  selectTaskState,
  (state: TaskState) => state.error
);
