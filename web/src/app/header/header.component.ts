import { Component, OnInit, HostListener } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  navigation: any;

  constructor() { }

  ngOnInit() {
    this.navigation = document.getElementById("navigation");
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    const number = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    if (number < 50) {
      this.navigation.style.height = "65px";
    } else if (number >= 50) {
      this.navigation.style.height = "75px";
    }
  }
}
