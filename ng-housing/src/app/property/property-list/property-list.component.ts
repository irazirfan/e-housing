import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {

  properties: Array<any> = [
    {
      "Id": 1,
      "Name":"Dream House",
      "Type":"House",
      "Price":12000
    },
    {
      "Id": 2,
      "Name":"Brishti Bilash",
      "Type":"House",
      "Price":10000
    },
    {
      "Id": 3,
      "Name":"Eastern Villa",
      "Type":"House",
      "Price":12500
    },
    {
      "Id": 3,
      "Name":"Asian Villa",
      "Type":"House",
      "Price":16000
    },
    {
      "Id": 3,
      "Name":"Eastern Palace",
      "Type":"House",
      "Price":12500
    },
    {
      "Id": 3,
      "Name":"North Breeze",
      "Type":"House",
      "Price":12500
    }
  ]


  constructor() { }

  ngOnInit(): void {
  }

}
