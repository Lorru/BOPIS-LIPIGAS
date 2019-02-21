import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    roles: Array<any> = new Array<any>();
    roleId: Number = 1;
    profileId: Number;
    localId: Number;


  constructor(private router: Router) { }

  ngOnInit() {

    this.roles = JSON.parse(localStorage.getItem('roles'));
    this.profileId = JSON.parse(localStorage.getItem('userExisting')) == null ? 0 : JSON.parse(localStorage.getItem('userExisting')).profileId;
    this.localId = JSON.parse(localStorage.getItem('userExisting')) == null ? 0 : JSON.parse(localStorage.getItem('userExisting')).localId;
    this.validateProfile();

  }

  validateProfile(){

    this.roles = this.roles.filter(res => res.id == this.roleId);
    let length: Number = this.roles.length;

    if(length == 0){

      this.router.navigate(['/']);

    }

  }
}
