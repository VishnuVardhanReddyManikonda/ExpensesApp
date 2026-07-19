import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private apiUrl = 'https://localhost:7298/';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(
      this.apiUrl + 'api/Transactions/All'
    );
  }

  getById(id: number): Observable<Transaction> {
    return this.http.get<Transaction>(
      this.apiUrl + 'api/Transactions/Details/' + id
    );
  }

  create(transaction: any): Observable<Transaction> {
    return this.http.post<Transaction>(
      this.apiUrl + 'api/Transactions/Create',
      transaction
    );
  }

  update(id: number, transaction: Transaction): Observable<Transaction> {
    return this.http.put<Transaction>(
      this.apiUrl + 'api/Transactions/Update/' + id,
      transaction
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(
      this.apiUrl + 'api/Transactions/Delete/' + id
    );
  }
}