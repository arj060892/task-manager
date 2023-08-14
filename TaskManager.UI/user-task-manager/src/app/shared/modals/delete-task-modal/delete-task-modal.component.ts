import { Component, Input } from '@angular/core';
import { UserTaskResponseDTO } from '../../openapi/v1';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-task-modal',
  templateUrl: './delete-task-modal.component.html',
  styleUrls: ['./delete-task-modal.component.scss'],
})
export class DeleteTaskModalComponent {
  @Input() task?: UserTaskResponseDTO;

  constructor(public activeModal: NgbActiveModal) {}

  confirmDelete() {
    this.activeModal.close(this.task!.id);
  }
}
