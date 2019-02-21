import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.css']
})
export class OptionsComponent implements OnInit {

  roles: Array<any> = new Array<any>();
  roleId: Number = 3;
  localId: Number;

  constructor(private router: Router) { }

  ngOnInit() {

    this.roles = JSON.parse(localStorage.getItem('roles'));
    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;
    this.validateProfile();

  }

  validateProfile(){

    this.roles = this.roles.filter(res => res.id == this.roleId);
    let length: Number = this.roles.length;

    if(length == 0 || this.localId == null){

      this.router.navigate(['/menu/home']);

    }

  }
}
