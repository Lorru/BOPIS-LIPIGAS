import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-detail-order',
  templateUrl: './modal-detail-order.component.html',
  styleUrls: ['./modal-detail-order.component.css']
})
export class ModalDetailOrderComponent implements OnInit {

  order: any = {};

  loadingFindById: Boolean = false;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

}
