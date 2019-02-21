import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-configurations',
  templateUrl: './configurations.component.html',
  styleUrls: ['./configurations.component.css']
})
export class ConfigurationsComponent implements OnInit {

      roles: Array<any> = new Array<any>();
      roleId: Number = 8;
    
    
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
