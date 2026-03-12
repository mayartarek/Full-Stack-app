import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Demo';
  isCollapsed = false;
  public header: boolean = false;
  public sideBar: boolean = false;
  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }
  constructor(private router: Router) {}
  ngOnInit() {
    this.router.events.subscribe((data: any) => {
      console.log(data.url);
      if (data.url != undefined) {
        if (data.url === '/auth/signin') {
          this.header = false;
          this.sideBar = false;
        } else {
          this.header = true;
          this.sideBar = true;
        }
      }
    });
  }
}
