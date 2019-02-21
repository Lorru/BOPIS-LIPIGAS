import { Component, OnInit } from '@angular/core';
import { ConfigurationService } from 'src/app/services/configuration.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-table-configurations',
  templateUrl: './table-configurations.component.html',
  styleUrls: ['./table-configurations.component.css'],
  providers:[
    ConfigurationService
  ]
})
export class TableConfigurationsComponent implements OnInit {

  loadingFindAllPaged: Boolean = false;

  currentPage: Number = 1;
  countRowsByPage: Number;
  countRows: Number;
  sort: Number = 1;

  column: String = 'Id';

  sorts: any = {};

  configurations: Array<any> = new Array<any>();

  constructor(private _configurationService: ConfigurationService, private _toastrService: ToastrService, private router: Router, private _modalService: NgbModal) { this.countRowsByPage = environment.countRowsByPage; }

  ngOnInit() {

    this.fillSorts();
    this.findAllPaged();

  }

  fillSorts(){

    this.sorts.Id = 'fa fa-sort';
    this.sorts.Description = 'fa fa-sort';
    this.sorts.Key = 'fa fa-sort';

  }

  returnClassSiwtch(readOnly: Boolean){

    if(readOnly){

      return 'switch-true';

    }else{

      return 'switch-false';
    }

  }

  returnClassSmall(readOnly: Boolean){

    if(readOnly){

      return 'small-true';

    }else{
      
      return 'small-false';
    }

  }

  returnTypeInput(dataType: String){

    if(dataType == 'NUMERIC'){

      return 'number';

    }else if(dataType == 'VARCHAR'){

      return 'text';

    }

  }

  returnClassInput(readOnly: Boolean){

    if(readOnly){

      return 'form-control-plaintext';

    }else{
      
      return 'form-control';
    }

  }

  findAllPaged(){

    this.loadingFindAllPaged = true;

    this._configurationService.findAllPaged(this.currentPage, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;

      }else if(res.statusCode == 403){

        this.loadingFindAllPaged = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAllPaged = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAllPaged = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  pageChange(e){

    this.currentPage = e;
    
    this.loadingFindAllPaged = true;

    this._configurationService.findAllPaged(this.currentPage, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;
        this.currentPage = res.currentPage;

      }else if(res.statusCode == 204){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;

      }else if(res.statusCode == 403){

        this.loadingFindAllPaged = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAllPaged = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAllPaged = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  orderByAndSort(){

    this.loadingFindAllPaged = true;

    this.currentPage = 1;
    this.sort = this.sort == 1 ? 0 : 1;

    this.sortUpAndDown();

    this._configurationService.findAllPaged(this.currentPage, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAllPaged = false;
        this.configurations = res.configurations;

      }else if(res.statusCode == 403){

        this.loadingFindAllPaged = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAllPaged = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAllPaged = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  sortUpAndDown(){


    if(this.column == "Id" && this.sort == 1){

      this.fillSorts();
      this.sorts.Id = 'fa fa-sort-up';

    }else if(this.column == "Id" && this.sort == 0){

      this.fillSorts();
      this.sorts.Id = 'fa fa-sort-down';

    }else if(this.column == "Description" && this.sort == 1){

      this.fillSorts();
      this.sorts.Description = 'fa fa-sort-up';

    }else if(this.column == "Description" && this.sort == 0){

      this.fillSorts();
      this.sorts.Description = 'fa fa-sort-down';

    }else if(this.column == "Key" && this.sort == 1){

      this.fillSorts();
      this.sorts.Key = 'fa fa-sort-up';

    }else if(this.column == "Key" && this.sort == 0){

      this.fillSorts();
      this.sorts.Key = 'fa fa-sort-down';

    }

  }

  updateReadOnlyFindbyIdAndStatusEqualToOne(i: number){

    this.configurations[i].readOnly = this.configurations[i].readOnly == true ? false : true;

    this._configurationService.updateValueAndReadOnlyByIdAndStatusEqualToOne(this.configurations[i].id, this.configurations[i].value, this.configurations[i].readOnly).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  updateValueAndReadOnlyByIdAndStatusEqualToOne(i: number){


    if(this.configurations[i].value == ''){

      this.configurations[i].value = this.configurations[i].dataType == 'NUMERIC' ? 0 : '';

    }

    this._configurationService.updateValueAndReadOnlyByIdAndStatusEqualToOne(this.configurations[i].id, this.configurations[i].value, this.configurations[i].readOnly).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
