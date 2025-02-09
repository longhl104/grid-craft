import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';
import { GridColumnInput, IGridInput } from './shared/apis/generated-apis';

@Component({
  selector: 'app-root',
  imports: [FormsModule, InputTextModule, FloatLabel],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'grid-craft-angular';

  gridInput: IGridInput = {
    numberOfRows: 1000,
    columns: [
      new GridColumnInput({
        name: 'Date',
        expression: '{2020 + Truncate(i / 4) }-Q{i % 4 + 1}',
      }),
      new GridColumnInput({
        name: 'Demand',
        expression: '{Rand(4000, 6000, 1)}',
      }),
      new GridColumnInput({
        name: 'Marketing Spend',
        expression: '{Rand(8000, 20000, 1000)}',
      }),
      new GridColumnInput({
        name: 'GDP Growth',
        expression: '{Rand(2, 4, 0.1)}%',
      }),
      new GridColumnInput({
        name: 'Holiday Season',
        expression: '{Rand(0, 1, 1)}',
      }),
    ],
  };
}
