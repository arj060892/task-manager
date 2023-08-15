import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { Store, StoreModule } from '@ngrx/store';
import { ToastrService, ToastrModule } from 'ngx-toastr';
import { Actions } from '@ngrx/effects';
import { UserTaskResponseDTO } from '../shared/openapi/v1';
import * as TaskActions from '../shared/store/actions/task.actions';
import * as fromTasks from '../shared/store/selectors/task.selectors';
import { of } from 'rxjs';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let store: Store;
  let router: Router;
  let modalService: NgbModal;
  let toastr: ToastrService;
  let actions$: Actions;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskListComponent],
      imports: [NgbModule, ToastrModule.forRoot(), StoreModule.forRoot({})],
      providers: [
        {
          provide: Router,
          useValue: { navigate: jasmine.createSpy('navigate') },
        },
        {
          provide: Actions,
          useValue: { pipe: jasmine.createSpy('pipe').and.returnValue(of()) },
        },
      ],
    });

    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    store = TestBed.inject(Store);
    router = TestBed.inject(Router);
    modalService = TestBed.inject(NgbModal);
    toastr = TestBed.inject(ToastrService);
    actions$ = TestBed.inject(Actions);

    spyOn(store, 'dispatch').and.callThrough();
    spyOn(store, 'select').and.returnValue(of([]));
    spyOn(toastr, 'success').and.callThrough();
    spyOn(toastr, 'error').and.callThrough();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should classify tasks into today, upcoming, and past based on their due dates when ngOnInit is called', () => {
    const today = new Date();
    const tasks: UserTaskResponseDTO[] = [
      { id: 1, dueDate: today.toISOString() },
      {
        id: 2,
        dueDate: new Date(today.setDate(today.getDate() + 1)).toISOString(),
      },
      {
        id: 3,
        dueDate: new Date(today.setDate(today.getDate() - 2)).toISOString(),
      },
    ];
    store.select = jasmine.createSpy().and.returnValue(of(tasks));
    component.ngOnInit();
    expect(component.todayTasks.length).toBe(1);
    expect(component.upcomingTasks.length).toBe(1);
    expect(component.pastTasks.length).toBe(1);
  });

  it('should navigate to the task edit page when editTask is called', () => {
    const task: UserTaskResponseDTO = {
      id: 1,
      dueDate: new Date().toISOString(),
    };
    component.editTask(task);
    expect(router.navigate).toHaveBeenCalledWith(['/task', task.id]);
  });

  it('should open the delete task modal and dispatch delete action when a task is confirmed to be deleted', async () => {
    const task: UserTaskResponseDTO = {
      id: 1,
      dueDate: new Date().toISOString(),
    };
    const modalRef = {
      componentInstance: {},
      result: Promise.resolve(task.id),
    } as any;
    spyOn(modalService, 'open').and.returnValue(modalRef);
    component.deleteTask(task);
    await modalRef.result;
    expect(store.dispatch).toHaveBeenCalledWith(
      TaskActions.deleteTask({ taskId: task.id! })
    );
  });

  it('should dispatch loadTasks and show a success toast when deleteTaskSuccess action is received', () => {
    actions$.pipe = jasmine
      .createSpy()
      .and.returnValue(of(TaskActions.deleteTaskSuccess({ taskId: 1 })));
    component.ngOnInit();

    expect(store.dispatch).toHaveBeenCalledWith(TaskActions.loadTasks());

    expect(toastr.success).toHaveBeenCalledWith('Task deleted successfully');
  });

  it('should show an error toast when deleteTaskFailure action is received', () => {
    const error = { error: { error: 'Test Error' } };
    actions$.pipe = jasmine
      .createSpy()
      .and.returnValue(of(TaskActions.deleteTaskFailure({ error })));
    component.ngOnInit();
    expect(toastr.error).toHaveBeenCalledWith(Object({ error: 'Test Error' }));
  });
});
