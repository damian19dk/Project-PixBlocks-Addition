import {Form} from './form';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';

export abstract class FormModal extends Form {
  protected constructor(protected modalService: NgbModal,
                        protected modalConfig: NgbModalConfig) {
    super();
    modalConfig.backdrop = 'static';
    modalConfig.keyboard = false;
  }

  openModal(content) {
    this.resetFlags();
    this.initFormModal();
    this.modalService.open(content, {centered: true});
  }

  closeModal(modal) {
    modal.close('Close click');
  }

  abstract initFormModal();
}
