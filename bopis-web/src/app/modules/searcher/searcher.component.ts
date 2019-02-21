import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searcher',
  templateUrl: './searcher.component.html',
  styleUrls: ['./searcher.component.css']
})
export class SearcherComponent implements OnInit {

  roles: Array<any> = new Array<any>();
  roleId: Number = 2;


  constructor(private router: Router) { }

  ngOnInit() {

    this.roles = JSON.parse(localStorage.getItem('roles'));
    this.validateProfile();

  }

  validateProfile(){

    this.roles = this.roles.filter(res => res.id == this.roleId);
    let length: Number = this.roles.length;

    if(length == 0){

      this.router.navigate(['/menu/home']);

    }

  }

}
