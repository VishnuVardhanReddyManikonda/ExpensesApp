import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction';

@Component({
  selector: 'app-transaction-list',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css'
})
export class TransactionList implements OnInit {
  transactions: Transaction[] = [];
  isLoading: boolean = true;
  errorMessage: string = '';

  constructor(private transactionService: TransactionService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions(): void {
  this.isLoading = true;
  this.errorMessage = '';
  
  this.transactionService.getAll().subscribe({
    next: (data) => {
      this.transactions = Array.isArray(data) ? data.reverse() : [];
      this.cdr.markForCheck();
      setTimeout(() => {
        this.isLoading = false;
        this.cdr.markForCheck();
      }, 100);
    },
    error: (error) => {
      this.errorMessage = `Failed to load transactions: ${error?.status} ${error?.statusText}`;
      this.isLoading = false;
    }
  });
}

  getTotalIncome(): number {
    return this.transactions
      .filter(t => t.type?.toLowerCase() === 'income')
      .reduce((sum, t) => sum + t.amount, 0);
  }

  getTotalExpenses(): number {
    return this.transactions
      .filter(t => ['expense', 'expenditure'].includes(t.type?.toLowerCase() || ''))
      .reduce((sum, t) => sum + t.amount, 0);
  }

  getNetBalance(): number {
    return this.getTotalIncome() - this.getTotalExpenses();
  }
}