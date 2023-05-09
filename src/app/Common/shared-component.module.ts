import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RatingComponent} from '../Common/rating/rating.component';
import { FormsModule } from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button'
import {MatBadgeModule} from '@angular/material/badge';
import {DragDropModule} from '@angular/cdk/drag-drop';



@NgModule({
  declarations: [RatingComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,MatButtonModule,DragDropModule,MatBadgeModule
  ],
  exports:[RatingComponent,MatCardModule,MatButtonModule,DragDropModule,MatBadgeModule]
})
export class SharedComponentModule { }
