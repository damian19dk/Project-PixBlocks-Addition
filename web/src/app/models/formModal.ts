import {Form} from './form';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

export class FormModal extends Form {
  constructor(protected modalService: NgbModal) {
    super();
  }

  openModal(content) {
    this.modalService.open(content, {centered: true});
  }
}
