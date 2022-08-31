import { Injectable } from '@angular/core';
import {NgbdModalContentComponent} from "../ngbd-modal-content/ngbd-modal-content.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(
    private modalService: NgbModal
  ) { }
  public open(title: string, content: string, fun: Function | null = null): void{
    const modalRef = this.modalService.open(NgbdModalContentComponent);
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.content = content;
    modalRef.componentInstance.okClicked = fun;
    modalRef.componentInstance.closeClicked = fun;
  }
}
