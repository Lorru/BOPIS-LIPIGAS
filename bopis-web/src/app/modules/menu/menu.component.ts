import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  token: String;
  roles: Array<any> = new Array<any>();
  localId: Number;

  constructor(private router: Router) { }

  ngOnInit() {

    this.token = localStorage.getItem('token');
    this.roles = JSON.parse(localStorage.getItem('roles'));
    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;
    this.validateToken();

  }

  validateToken(){

    if(this.token == null || this.token == ''){

      this.router.navigate(['/']);

    }

  }

  logOut(){

    this.router.navigate(['/']);
    localStorage.clear();

  }

}
