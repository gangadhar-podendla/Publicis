import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Medicine } from './models/medicine';
import { MedicineService } from './services/medicine.service';
import { SaleService } from './services/sale.service';
import { Sale } from './models/sale';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  medicines: Medicine[] = [];

  searchText : string | undefined = undefined;

  showAddMedicineModal = false;

  newMedicine: Medicine = {
    id: 0,
    fullName: '',
    notes: '',
    expiryDate: '',
    quantity: 0,
    price: 0,
    brand: ''
  };

sales: Sale[] = [];

showSellModal = false;

selectedMedicine: Medicine | null = null;

saleQuantity = 1;

  constructor(
    private medicineService: MedicineService,
     private cdr: ChangeDetectorRef,
      private saleService: SaleService,
  ) {}

  ngOnInit(): void {
    this.loadMedicines();
    this.loadSales();
  }
  loadSales(): void {

  this.saleService
    .getSales()
    .subscribe({
      next: data => {

        this.sales = data;

        this.cdr.detectChanges();
      },

      error: error => {
        console.error(
          'Error loading sales:',
          error
        );
      }
    });
}
openSellModal(medicine: Medicine): void {

  this.selectedMedicine = medicine;

  this.saleQuantity = 1;

  this.showSellModal = true;
}
closeSellModal(): void {

  this.showSellModal = false;

  this.selectedMedicine = null;

  this.saleQuantity = 1;

  this.cdr.detectChanges();
}
sellMedicine(): void {

  if (!this.selectedMedicine) {
    return;
  }

  this.saleService
    .sellMedicine({
      medicineId: this.selectedMedicine.id,
      quantity: this.saleQuantity
    })
    .subscribe({
      next: () => {

        this.showSellModal = false;

        this.selectedMedicine = null;

        this.saleQuantity = 1;

        this.loadMedicines();

        this.loadSales();

        this.cdr.detectChanges();
      },

      error: error => {
        console.error(
          'Error selling medicine:',
          error
        );
      }
    });
}
  loadMedicines(): void {

    this.medicineService
      .getMedicines(this.searchText)
      .subscribe({
        next: (data: Medicine[]) => {
          this.medicines = data;
          this.cdr.detectChanges();
        },

        error: error => {
          console.error(
            'Error loading medicines: from app',
            error
          );
        }
      });
  }

  search(searchTerm: string | undefined): void {
    this.searchText = searchTerm || '';
    this.loadMedicines();
  }

  openAddMedicineModal(): void {
    this.showAddMedicineModal = true;
  }

  closeAddMedicineModal(): void {
    this.showAddMedicineModal = false;
    this.resetMedicineForm();
    this.cdr.detectChanges();
  }

  addMedicine(): void {

    this.medicineService
      .addMedicine(this.newMedicine)
      .subscribe({
        next: () => {

          this.closeAddMedicineModal();

          this.loadMedicines();
          this.cdr.detectChanges();
        },

        error: error => {
          console.error(
            'Error adding medicine:',
            error
          );
        }
      });
  }

  resetMedicineForm(): void {

    this.newMedicine = {
      id: 0,
      fullName: '',
      notes: '',
      expiryDate: '',
      quantity: 0,
      price: 0,
      brand: ''
    };
  }

  isExpiringSoon(expiryDate: string): boolean {

    const today = new Date();

    const expiry = new Date(expiryDate);

    const difference =
      expiry.getTime() - today.getTime();

    const days =
      difference / (1000 * 60 * 60 * 24);

    return days < 30;
  }

  isLowStock(quantity: number): boolean {
    return quantity < 10;
  }
}