import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuComponent } from './menu.component';
import { HomeComponent } from '../home/home.component';
import { SearcherComponent } from '../searcher/searcher.component';
import { OptionsComponent } from '../options/options.component';
import { PricesComponent } from '../prices/prices.component';
import { LocalsComponent } from '../locals/locals.component';
import { UsersComponent } from '../users/users.component';
import { ReportsComponent } from '../reports/reports.component';
import { ConfigurationsComponent } from '../configurations/configurations.component';

const routes: Routes = [
  { path: 'menu', component:  MenuComponent,  children: [
    { path: 'home', component:  HomeComponent },
    { path: 'searcher', component:  SearcherComponent },
    { path: 'options',  component: OptionsComponent },
    { path: 'prices', component:  PricesComponent },
    { path: 'locals', component:  LocalsComponent },
    { path: 'users',  component:  UsersComponent  },
    { path: 'reports',  component:  ReportsComponent  },
    { path: 'configurations', component:  ConfigurationsComponent }
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuRoutingModule { }
