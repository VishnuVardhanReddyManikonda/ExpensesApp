import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction';

@Component({
  selector: 'app-transaction-form',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true,
  templateUrl: './transaction-form.html',
  styleUrl: './transaction-form.css'
})
export class TransactionForm implements OnInit {
  transactionForm: FormGroup;
  
  incomeCategories = ['Salary', 'Freelance', 'Investment'];
  expenseCategories = ['Food', 'Transportation', 'Entertainment'];
  availableCategories: string[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private transactionService: TransactionService
  ) {
    const today = new Date().toISOString().split('T')[0];
    this.transactionForm = this.fb.group({
      type: ['Expense', Validators.required],
      category: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(0)]],
      createdAt: [today, Validators.required]
    });
  }

  ngOnInit(): void {
    this.updateAvailableCategories();
    
    this.transactionForm.get('type')?.valueChanges.subscribe(() => {
      this.onTypeChange();
    });
  }

  onTypeChange(): void {
    this.updateAvailableCategories();
    this.transactionForm.patchValue({ category: '' });
  }

  updateAvailableCategories(): void {
    const type = this.transactionForm.get('type')?.value;
    this.availableCategories = type === 'Expense' 
      ? this.expenseCategories 
      : this.incomeCategories;
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      
      const formValue = this.transactionForm.value;
      
      const transaction = {
        type: formValue.type,
        category: formValue.category,
        amount: parseFloat(formValue.amount),
        createdAt: formValue.createdAt
      };
      
      this.transactionService.create(transaction as any).subscribe({
        next: (data) => {
          this.isLoading = false;
          this.router.navigate(['/transactions']);
        },
        error: (error) => {
          this.errorMessage = `Failed to create transaction: ${error?.status || 'Unknown error'}`;
          this.isLoading = false;
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/transactions']);
  }
}