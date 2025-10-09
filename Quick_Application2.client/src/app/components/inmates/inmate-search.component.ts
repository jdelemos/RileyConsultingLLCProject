import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-inmates-search',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './inmates-search.component.html',
  styleUrls: ['./inmates-search.component.scss']
})
export class InmatesSearchComponent {
  inmates: any[] = [];
  filters = { firstName: '', lastName: '' };
  private apiUrl = '/api/v1/inmates/search';

  constructor(private http: HttpClient) { }

  searchInmates(): void {
    let params = new HttpParams();
    Object.keys(this.filters).forEach(key => {
      if (this.filters[key]) params = params.set(key, this.filters[key]);
    });

    this.http.get<any[]>(this.apiUrl, { params }).subscribe({
      next: (data) => this.inmates = data,
      error: (err) => console.error('Error fetching inmates:', err)
    });
  }
}
