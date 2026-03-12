import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/Core/Services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

constructor(public authService: AuthService) { }
@Output() toggleSidebar = new EventEmitter();

menuOpen=false;

toggleMenu(){
 this.menuOpen=!this.menuOpen;
}

logout(){
 this.authService.logOut();
}
}
