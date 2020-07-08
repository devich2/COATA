import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UnitTreeComponent } from './core/components/unit-tree/unit-tree.component';


const routes: Routes = [
  { path: '**', component: UnitTreeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
