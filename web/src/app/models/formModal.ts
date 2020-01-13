import {Form} from './form';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';

export class FormModal extends Form {
  constructor(protected modalService: NgbModal,
              protected config: NgbModalConfig) {
    super();
    config.backdrop = 'static';
    config.keyboard = false;
  }

  openModal(content) {
    this.modalService.open(content, {centered: true});
  }
}
