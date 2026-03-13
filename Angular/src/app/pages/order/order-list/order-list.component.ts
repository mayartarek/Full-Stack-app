import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DataService } from 'src/Core/Services/data.service';
import { getOrder } from 'src/Core/constant/api.constant';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent {
 orderItems: any[] = [];
constructor(private router: Router,private dataService: DataService) { } 
     httpOptions = {
        headers: new HttpHeaders({ "Content-Type": "application/json" }),
    };
    
  ngOnInit() {  
    this.getOrders();
  }
  getOrders(){
    this.dataService.getList(getOrder,this.httpOptions).subscribe((res:any)=>{
      this.orderItems = res;
    });
  }
  
}
