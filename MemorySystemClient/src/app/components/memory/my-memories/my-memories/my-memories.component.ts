import { Component, OnInit } from '@angular/core';

import { MemoryService } from 'src/app/services/memory/memory.service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';

@Component({
  selector: 'app-my-memories',
  templateUrl: './my-memories.component.html',
  styleUrls: ['./my-memories.component.css'],
})
export class MyMemoriesComponent implements OnInit {
  public pictures: any[] = [];
  public page: number = 1;
  
  constructor(private memoryService: MemoryService, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
    let key = this.localStorageService.getItem('my-memory-category');
    if (!key) {
      key = 'All'; 
    }

    this.memoryService.myMemories(key).subscribe(data => { this.pictures = data.data; debugger; });
  }
}
