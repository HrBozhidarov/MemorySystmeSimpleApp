import { Component, OnInit } from '@angular/core';

import { MemoryService } from 'src/app/services/memory/memory.service';

@Component({
  selector: 'app-my-memories',
  templateUrl: './my-memories.component.html',
  styleUrls: ['./my-memories.component.css']
})
export class MyMemoriesComponent implements OnInit {
  public memories: any[] = [];
  constructor(private memoryService: MemoryService) { }

  ngOnInit(): void {
    this.memoryService.myMemories().subscribe(data => this.memories = data);
  }
}
