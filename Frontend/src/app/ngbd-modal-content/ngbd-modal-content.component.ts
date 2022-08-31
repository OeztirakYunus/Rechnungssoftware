import {Component, EventEmitter, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-ngbd-modal-content',
  templateUrl: './ngbd-modal-content.component.html',
  styleUrls: ['./ngbd-modal-content.component.css']
})
export class NgbdModalContentComponent implements OnInit {

  @Input() title: string = '';
  @Input() content: string = '';
  @Input() okClicked: Function | null = null;
  @Input() closeClicked: Function | null = null;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit(): void {
  }

  close() {
    this.activeModal.dismiss('Cross click');
    if(this.closeClicked != null){
      this.closeClicked();
    }
  }

  ok() {
    this.activeModal.close('Close click');
    if(this.okClicked != null){
      this.okClicked();
    }
  }
}
