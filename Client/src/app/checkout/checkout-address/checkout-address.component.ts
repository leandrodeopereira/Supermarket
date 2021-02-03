import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IAddress } from 'src/app/shared/models/address';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  saveUserAddres(): void {
    this.accountService
      .updateUserAddress(this.checkoutForm.get('addressForm').value)
      .subscribe(
        (address: IAddress) => {
          this.toastr.success('Address saved');
          this.checkoutForm.get('addressForm').reset(address);
        },
        (error) => {
          this.toastr.error(error.message);
          console.log(error);
        }
      );
  }
}
