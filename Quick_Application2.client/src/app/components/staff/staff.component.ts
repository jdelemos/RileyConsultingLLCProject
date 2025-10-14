// src/app/components/staff/staff.component.ts
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common'; 

type Priority = 'low' | 'medium' | 'high';

export interface QuickAction {
  id: string;
  label: string;
  icon: string; // material icon name
}

export interface Inspection {
  id: string;
  type: string;
  block: string;
  cell: string;
  priority: Priority;
  status: string;
  statusTime: string; // e.g., "2h ago"
}

export interface ActivityLogItem {
  id: string;
  text: string;
  subtext: string; // e.g., "Officer Alvarez"
  time: string;    // e.g., "09:41"
}

export interface StatCard {
  value: string | number;
  label: string;
  sublabel?: string;
}

@Component({
  selector: 'app-staff',
  standalone: true,                             // needed for loadComponent lazy routes
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.scss'],
  imports: [CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StaffComponent {
  // ----- Data backing the template -----
  quickActions: QuickAction[] = [];
  cellInspections: Inspection[] = [];
  dueInspections: Inspection[] = [];
  recentActivity: ActivityLogItem[] = [];
  stats: StatCard[] = [];

  // ----- Click handlers (no-ops for now) -----
  onQuickAction(action: QuickAction): void { }
  viewAllInspections(): void { }
  onInspectionClick(inspection: Inspection): void { }
  viewAllActivity(): void { }
  onActivityClick(activity: ActivityLogItem): void { }

  // ----- Utility used by [ngClass] in template -----
  getPriorityClass(priority: Priority): string {
    switch (priority) {
      case 'high': return 'priority-high';
      case 'medium': return 'priority-medium';
      case 'low': return 'priority-low';
      default: return '';
    }
  }
}
