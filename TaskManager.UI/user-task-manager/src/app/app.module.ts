import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ApiModule, Configuration } from './shared/openapi/v1';
import { HttpClientModule } from '@angular/common/http';
import { TaskListComponent } from './task-list/task-list.component';
import { TimeFormatPipe } from './shared/pipes/time-format/time-format.pipe';
import { DeleteTaskModalComponent } from './shared/modals/delete-task-modal/delete-task-modal.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { taskReducer } from './shared/store/reducers/task.reducer';
import { TaskEffects } from './shared/store/effects/task.effects';
import { TaskCreateComponent } from './task-create/task-create.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    TimeFormatPipe,
    DeleteTaskModalComponent,
    TaskCreateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    ApiModule.forRoot(() => {
      return new Configuration({
        basePath: `https://localhost:7129`,
      });
    }),
    HttpClientModule,
    StoreModule.forRoot({ task: taskReducer }),
    EffectsModule.forRoot([TaskEffects]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
