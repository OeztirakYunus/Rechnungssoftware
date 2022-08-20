import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Router } from '@angular/router';
import { DeliveryNote } from 'src/app/model/delivery-note.model';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-deliverynotes',
  templateUrl: './deliverynotes.component.html',
  styleUrls: ['./deliverynotes.component.css']
})


export class DeliverynotesComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['DeliveryNoteNumber', 'DeliveryNoteDate', 'Status'];
  // dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  dataSource!: MatTableDataSource<DeliveryNote>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  public constructor(private httpService : HttpService){
  }
  async ngOnInit() {
    await this.httpService.getDeliveryNotes();
    console.log(this.httpService.deliveryNotes);
    this.dataSource = new MatTableDataSource(this.httpService.deliveryNotes);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  //@ViewChild(MatPaginator) paginator!: MatPaginator;

//   ngAfterViewInit() {
//     this.dataSource.paginator = this.paginator;
//   }
}

