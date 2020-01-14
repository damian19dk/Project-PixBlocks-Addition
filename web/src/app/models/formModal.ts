import {Form} from './form';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';

export class FormModal extends Form {
  constructor(protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig) {
    super();
    modalConfig.backdrop = 'static';
    modalConfig.keyboard = false;
  }

  openModal(content) {
    this.modalService.open(content, {centered: true});
  }
}
