import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { UserTasksService } from '../../openapi/v1';
import * as TaskActions from '../actions/task.actions';

@Injectable()
export class TaskEffects {
  constructor(
    private actions$: Actions,
    private tasksService: UserTasksService
  ) {}

  loadTasks$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(TaskActions.loadTasks),
      mergeMap(() =>
        this.tasksService.apiUserTasksGet().pipe(
          map((tasks) => TaskActions.loadTasksSuccess({ tasks })),
          catchError((error) => of(TaskActions.loadTasksFailure({ error })))
        )
      )
    );
  });

  createTask$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(TaskActions.createTask),
      mergeMap((action) =>
        this.tasksService.apiUserTasksPost(action.task).pipe(
          map((task) => TaskActions.createTaskSuccess({ task })),
          catchError((error) => of(TaskActions.createTaskFailure({ error })))
        )
      )
    );
  });

  updateTask$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(TaskActions.updateTask),
      mergeMap((action) =>
        this.tasksService
          .apiUserTasksIdPut(action.taskId, action.updatedTask)
          .pipe(
            map((updatedTask) =>
              TaskActions.updateTaskSuccess({ updatedTask })
            ),
            catchError((error) => of(TaskActions.updateTaskFailure({ error })))
          )
      )
    );
  });

  deleteTask$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(TaskActions.deleteTask),
      mergeMap((action) =>
        this.tasksService.apiUserTasksIdDelete(action.taskId).pipe(
          map(() => TaskActions.deleteTaskSuccess({ taskId: action.taskId })),
          catchError((error) => of(TaskActions.deleteTaskFailure({ error })))
        )
      )
    );
  });

  loadTaskById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TaskActions.loadTaskById),
      mergeMap((action) =>
        this.tasksService.apiUserTasksIdGet(action.taskId).pipe(
          map((task) => TaskActions.loadTaskByIdSuccess({ task })),
          catchError((error) => of(TaskActions.loadTaskByIdFailure({ error })))
        )
      )
    )
  );
}
