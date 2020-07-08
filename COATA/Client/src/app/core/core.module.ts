import { NgModule, Provider } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { UnwraperInterceptor } from './api/interceptors/unwraper.interceptor';
import { RouterModule } from '@angular/router';
import { environment } from '../../environments/environment';
import { LoggingInterceptor } from './api/interceptors/logger.interceptor';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressBarModule } from 'primeng/progressbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { DataViewModule } from 'primeng/dataview';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDividerModule } from '@angular/material/divider';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UnitTreeComponent } from './components/unit-tree/unit-tree.component';
import { MatTreeModule } from '@angular/material/tree';
import { UnitApiService } from './api/unit/unit-api.service';
import { ClassificationApiService } from './api/unit/classification-api.service';

const interceptors: Provider[] = [{ provide: HTTP_INTERCEPTORS, useClass: UnwraperInterceptor, multi: true }];

if (!environment.production) {
  interceptors.push({ provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true });
}

@NgModule({
  declarations: [UnitTreeComponent],
  imports: [
    //Angular
    CommonModule, HttpClientModule, RouterModule, FontAwesomeModule,
    // Forms
    FormsModule, ReactiveFormsModule,
    // Material
    MatButtonModule, MatButtonToggleModule, MatTabsModule, MatInputModule,
    MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatSelectModule,
    MatSidenavModule, MatIconModule, MatDialogModule, MatExpansionModule, MatDividerModule, MatPaginatorModule,
    DragDropModule, MatProgressSpinnerModule, MatTreeModule,
    // Prime NG
    TableModule, ButtonModule, InputTextModule, DropdownModule, ProgressBarModule, DataViewModule,
    CalendarModule, MultiSelectModule,
    // Icons
    FontAwesomeModule
  ],
  exports: [
    // Angular
    CommonModule,
    // Forms
    FormsModule, ReactiveFormsModule,
    // Material
    MatButtonModule, MatButtonToggleModule, MatTabsModule, MatInputModule,
    MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatSelectModule,
    MatSidenavModule, MatIconModule, MatDialogModule, MatExpansionModule, MatDividerModule, MatPaginatorModule,
    DragDropModule, MatProgressSpinnerModule, MatTreeModule,
    // Prime NG
    TableModule, ButtonModule, InputTextModule, DropdownModule, ProgressBarModule, DataViewModule,
    CalendarModule, MultiSelectModule,
    // Icons
    FontAwesomeModule,
    UnitTreeComponent
  ],
  providers: [...interceptors, UnitApiService, ClassificationApiService]
})
export class CoreModule {
}
