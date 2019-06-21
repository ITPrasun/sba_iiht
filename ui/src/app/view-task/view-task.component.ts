import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Task } from '../models/task';

import { EventService } from '../services/event.service';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ProjectService } from '../services/project.service';
import { Project } from '../models/project';
import { TaskService } from '../services/task.service';

@Component({
  selector: 'app-view-task',
  templateUrl: './view-task.component.html',
  styleUrls: ['./view-task.component.css']
})
export class ViewTaskComponent implements OnInit {
  modalRef: BsModalRef;
  projects: Array<Project>;
  searchText: string;
  selectedIndex: number;
  selectedProjName: string;
  tasks: Array<Task> = [];
  taskSearch: boolean;
  isStartDateAsc: boolean;
  isEndDateAsc: boolean;
  isPriorityAsc: boolean;
  isCompletedAsc: boolean;

  constructor(private eventService: EventService, private projectService: ProjectService,
    private taskService: TaskService, private modalService: BsModalService, private router: Router) {
    this.projects = new Array<Project>();
  }

  ngOnInit() {
    this.eventService.showLoading(true);
    this.projectService.getProject().subscribe((project) => {
      this.projects = project;
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    document.getElementsByTagName("modal-container")[0].classList.remove("fade");
  }


  setIndex(index: number) {
    this.selectedIndex = index;
  }

  selectProj() {
    this.selectedProjName = this.projects[this.selectedIndex].projectName;
    this.taskService.getAllTasksByProjectId(+this.projects[this.selectedIndex].projectId).subscribe(
      (tasks) => {
        debugger;
        this.tasks = tasks;
        this.taskSearch = true;
        this.eventService.showLoading(false);
      },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
    this.modalRef.hide();

  }

  editTask(task) {
    this.router.navigate(['/addTask', { task: JSON.stringify(task) }]);
  }

  deleteTask(task) {
    this.eventService.showLoading(true);
    this.taskService.deleteTask(task).subscribe((data) => {
      this.eventService.showSuccess('Task completed successfully')
      this.selectProj();
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }

  sortTask(type: number) {
    if (type === 1) {
      this.isStartDateAsc = !this.isStartDateAsc;
      const direction = this.isStartDateAsc ? 1 : -1;
      this.tasks.sort((a, b) => (a.StartDate > b.StartDate) ? 1 * direction
        : ((b.StartDate > a.StartDate) ? -1 * direction : 0));
    }
    if (type === 2) {
      this.isEndDateAsc = !this.isEndDateAsc;
      const direction = this.isEndDateAsc ? 1 : -1;
      this.tasks.sort((a, b) => (a.EndDate > b.EndDate) ? 1 * direction :
        ((b.EndDate > a.EndDate) ? -1 * direction : 0));
    }
    if (type === 3) {
      this.isPriorityAsc = !this.isPriorityAsc;
      const direction = this.isPriorityAsc ? 1 : -1;
      this.tasks.sort((a, b) => (a.priority > b.priority) ? 1 * direction :
        ((b.priority > a.priority) ? -1 * direction : 0));
    }
    if (type === 4) {
      this.isCompletedAsc = !this.isCompletedAsc;
      const direction = this.isCompletedAsc ? 1 : -1;
      this.tasks.sort((a, b) => (a.Status> b.Status) ? 1 * direction :
        ((b.Status > a.Status) ? -1 * direction : 0));
    }
    this.tasks = [...this.tasks];
  }
}