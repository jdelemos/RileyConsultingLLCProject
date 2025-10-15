// src/app/figma-home/figma-home.component.ts
import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription, of } from 'rxjs';
import { delay } from 'rxjs/operators';

import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { Utilities } from '../../services/utilities';

interface QuickAction {
  icon: string;
  label: string;
  action: string;
}

interface Inspection {
  id: string;
  type: string;
  block: string;
  cell: string;
  status: string;
  statusTime: string;
  priority: 'high' | 'medium' | 'low';
}

interface Activity {
  id: string;
  text: string;
  subtext: string;
  time: string;
  type: 'medical' | 'transfer' | 'incident' | 'processing' | 'approval';
}

interface Stat {
  value: number;
  label: string;
  sublabel: string;
  trend?: 'up' | 'down' | 'neutral';
}

@Component({
  selector: 'app-figma-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './figma-home.component.html',
  styleUrl: './figma-home.component.scss'
})
export class FigmaHomeComponent implements OnInit, OnDestroy {

  public alertService = inject(AlertService);
  private configurations = inject(ConfigurationService);

  // Search and loading state
  searchQuery = '';
  isLoading = false;
  loadSub: Subscription | undefined;

  // Quick Actions
  quickActions: QuickAction[] = [];
  inmateActions: QuickAction[] = [];
  quickActions2: QuickAction[] = [];

  // Inspections
  cellInspections: Inspection[] = [];
  dueInspections: Inspection[] = [];

  // Activity Log
  recentActivity: Activity[] = [];

  // Statistics
  stats: Stat[] = [];

  ngOnInit(): void {
    this.initializeData();
    this.loadDashboardData();
  }

  ngOnDestroy(): void {
    this.loadSub?.unsubscribe();
  }

  private initializeData(): void {
    // Initialize Quick Actions
    this.quickActions = [
      { icon: 'shield', label: 'Cell Management', action: 'cell-management' },
      { icon: 'group', label: 'Staff Management', action: 'inmate-management' },
      { icon: 'warning', label: 'Medical Records', action: 'incident-reports' },
      { icon: 'bar_chart', label: 'Incident Reports', action: 'security-reports' },
      { icon: 'analytics', label: 'Analytics', action: 'analytics' },
      { icon: 'event', label: 'Calendar', action: 'scheduling' },
      // Add next to your existing quickActions array

    ];

    // Keep your existing quickActions as-is…

    this.quickActions2 = [
      { icon: 'add', label: 'Book New Inmate', action: 'book-new-inmate' },
      { icon: 'lock_open', label: 'Process Release', action: 'process-release' },
      { icon: 'description', label: 'Log Incident', action: 'log-incident' },
      { icon: 'sync_alt', label: 'Initiate Transfer', action: 'initiate-transfer' },
      { icon: 'badge', label: 'Schedule Visitation', action: 'schedule-visitation' },
      { icon: 'assignment', label: 'File Shift Report', action: 'file-shift-report' },
    ];


    // Initialize Inmate Actions
    this.inmateActions = [
      { icon: 'add', label: 'Book New Inmate', action: 'book-inmate' },
      { icon: 'lock', label: 'Permissions', action: 'permissions' },
      { icon: 'assignment', label: 'Log Incident', action: 'log-incident' },
      { icon: 'swap_horiz', label: 'Inmate Transfer', action: 'transfer' },
      { icon: 'camera_alt', label: 'Schedule Validation', action: 'validation' },
      { icon: 'assignment_turned_in', label: 'File WHS Report', action: 'whs-report' }
    ];

    // Initialize Statistics
    this.stats = [
      { value: 247, label: 'Total Inmates', sublabel: 'In facility today', trend: 'up' },
      { value: 18, label: 'Scheduled Events', sublabel: 'Today', trend: 'neutral' },
      { value: 13, label: 'Pending Transfers', sublabel: 'Awaiting approval', trend: 'down' },
      { value: 9, label: 'Incident Reports', sublabel: 'Last 24 hours', trend: 'up' }
    ];
  }

  loadDashboardData(): void {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', 'Loading dashboard data...');

    // Simulate API call
    this.loadSub = of({
      inspections: this.getMockInspections(),
      activities: this.getMockActivities()
    })
      .pipe(delay(800))
      .subscribe({
        next: (data) => {
          this.cellInspections = data.inspections.cell;
          this.dueInspections = data.inspections.due;
          this.recentActivity = data.activities;

          this.alertService.stopLoadingMessage();
          this.alertService.showMessage(
            'Dashboard Loaded',
            'Dashboard data loaded successfully',
            MessageSeverity.success
          );
          this.isLoading = false;
        },
        error: (err) => {
          this.handleError('Failed to load dashboard data', err);
        }
      });
  }

  private getMockInspections() {
    return {
      cell: [
        {
          id: '1',
          type: 'Cell Inspection',
          block: 'Block C',
          cell: 'Cell 15',
          status: 'Overdue',
          statusTime: 'Today 9:00 AM',
          priority: 'high' as const
        },
        {
          id: '2',
          type: 'New Intake Processing',
          block: 'Block A',
          cell: '',
          status: 'Today 3:00 PM',
          statusTime: 'Today 3:00 PM',
          priority: 'high' as const
        },
        {
          id: '3',
          type: 'Medical Escort',
          block: 'Inmate #4521 to Infirmary',
          cell: '',
          status: 'Tomorrow 1:00 PM',
          statusTime: 'Tomorrow 1:00 PM',
          priority: 'medium' as const
        },
        {
          id: '4',
          type: 'Visitor Processing',
          block: 'Johnson Family Visit',
          cell: '',
          status: 'Thursday 12:00 PM',
          statusTime: 'Thursday 12:00 PM',
          priority: 'medium' as const
        },
        {
          id: '5',
          type: 'Visitor Processing',
          block: 'Smith Family Visit',
          cell: '',
          status: 'Friday 1:00 PM',
          statusTime: 'Friday 1:00 PM',
          priority: 'low' as const
        },
        {
          id: '6',
          type: 'Equipment Check',
          block: 'Radio and Safety Gear',
          cell: '',
          status: 'Pending',
          statusTime: 'Saturday 10:00 AM',
          priority: 'low' as const
        },
        {
          id: '7',
          type: 'Equipment Check',
          block: 'Radio and Safety Gear',
          cell: '',
          status: 'Pending',
          statusTime: 'Sunday 10:00 AM',
          priority: 'low' as const
        },
        {
          id: '8',
          type: 'Cell Inspection',
          block: 'Block D',
          cell: 'Cell 8',
          status: 'Scheduled',
          statusTime: 'Monday 11:00 AM',
          priority: 'medium' as const
        },
        {
          id: '9',
          type: 'Medical Review',
          block: 'Block E',
          cell: 'Cell 20',
          status: 'Scheduled',
          statusTime: 'Monday 2:00 PM',
          priority: 'high' as const
        },
        {
          id: '10',
          type: 'Safety Check',
          block: 'Block F',
          cell: 'Cell 3',
          status: 'Pending',
          statusTime: 'Tuesday 9:00 AM',
          priority: 'low' as const
        },
        {
          id: '11',
          type: 'Intake Processing',
          block: 'Block G',
          cell: '',
          status: 'Scheduled',
          statusTime: 'Tuesday 3:00 PM',
          priority: 'medium' as const
        },
        {
          id: '12',
          type: 'Cell Transfer',
          block: 'Block H',
          cell: 'Cell 14',
          status: 'Scheduled',
          statusTime: 'Wednesday 10:00 AM',
          priority: 'high' as const
        },
        {
          id: '13',
          type: 'Maintenance Check',
          block: 'Block I',
          cell: 'Cell 7',
          status: 'Pending',
          statusTime: 'Wednesday 2:00 PM',
          priority: 'low' as const
        },
        {
          id: '14',
          type: 'Security Sweep',
          block: 'Block J',
          cell: 'All Cells',
          status: 'Scheduled',
          statusTime: 'Thursday 8:00 AM',
          priority: 'high' as const
        }
      ],
      due: [
        {
          id: '15',
          type: 'Cell Inspection',
          block: 'Block C',
          cell: 'Cell 15',
          status: 'Overdue',
          statusTime: 'Today 9:00 AM',
          priority: 'high' as const
        },
        {
          id: '16',
          type: 'New Intake Processing',
          block: 'Block A',
          cell: '',
          status: 'Today 3:00 PM',
          statusTime: 'Today 3:00 PM',
          priority: 'high' as const
        },
        {
          id: '17',
          type: 'Medical Escort',
          block: 'Inmate #4521 to Infirmary',
          cell: '',
          status: 'Tomorrow 1:00 PM',
          statusTime: 'Tomorrow 1:00 PM',
          priority: 'medium' as const
        },
        {
          id: '18',
          type: 'Medical Check',
          block: 'Block D',
          cell: 'Infirmary',
          status: 'Due Soon',
          statusTime: 'Tomorrow 2:00 PM',
          priority: 'high' as const
        },
        {
          id: '19',
          type: 'Medical Review',
          block: 'Health Unit',
          cell: 'Cell #12',
          status: 'Due Soon',
          statusTime: 'Tomorrow 3:00 PM',
          priority: 'high' as const
        },
        {
          id: '20',
          type: 'Standard',
          block: 'N/A',
          cell: 'Awaiting Assignment',
          status: 'Pending',
          statusTime: 'Tomorrow 4:00 PM',
          priority: 'low' as const
        },
        {
          id: '21',
          type: 'Cell Transfer',
          block: 'Block E',
          cell: 'Cell 22',
          status: 'Due Soon',
          statusTime: 'Friday 9:00 AM',
          priority: 'high' as const
        },
        {
          id: '22',
          type: 'Equipment Inspection',
          block: 'Block F',
          cell: '',
          status: 'Pending',
          statusTime: 'Friday 11:00 AM',
          priority: 'medium' as const
        },
        {
          id: '23',
          type: 'Security Review',
          block: 'Block G',
          cell: 'Cell 5',
          status: 'Due Soon',
          statusTime: 'Friday 2:00 PM',
          priority: 'high' as const
        },
        {
          id: '24',
          type: 'Maintenance Request',
          block: 'Block H',
          cell: 'Cell 18',
          status: 'Overdue',
          statusTime: 'Yesterday 3:00 PM',
          priority: 'high' as const
        },
        {
          id: '25',
          type: 'Safety Audit',
          block: 'Block I',
          cell: 'All Areas',
          status: 'Due Soon',
          statusTime: 'Saturday 10:00 AM',
          priority: 'high' as const
        },
        {
          id: '26',
          type: 'Visitor Check',
          block: 'Visitation Room',
          cell: '',
          status: 'Pending',
          statusTime: 'Saturday 1:00 PM',
          priority: 'low' as const
        },
        {
          id: '27',
          type: 'Cell Inspection',
          block: 'Block J',
          cell: 'Cell 9',
          status: 'Due Soon',
          statusTime: 'Sunday 9:00 AM',
          priority: 'medium' as const
        },
        {
          id: '28',
          type: 'Medical Follow-up',
          block: 'Block K',
          cell: 'Infirmary',
          status: 'Due Soon',
          statusTime: 'Sunday 2:00 PM',
          priority: 'high' as const
        },
        {
          id: '29',
          type: 'Intake Review',
          block: 'Processing',
          cell: '',
          status: 'Pending',
          statusTime: 'Monday 8:00 AM',
          priority: 'medium' as const
        },
        {
          id: '30',
          type: 'Security Briefing',
          block: 'All Staff',
          cell: '',
          status: 'Scheduled',
          statusTime: 'Monday 1:00 PM',
          priority: 'high' as const
        }
      ]
    };
  }

  private getMockActivities(): Activity[] {
    return [
      {
        id: 'a1',
        text: 'Inmate #0832 medical examination completed',
        subtext: 'Submitted by Officer T',
        time: '4 minutes ago',
        type: 'medical'
      },
      {
        id: 'a2',
        text: 'Transfer request initiated for Inmate J. Doe #1254',
        subtext: 'Transfer to Block C requested',
        time: '23 minutes ago',
        type: 'transfer'
      },
      {
        id: 'a3',
        text: 'Incident reported in cafeteria - Under investigation',
        subtext: 'Reported by Officer M',
        time: '1 hour ago',
        type: 'incident'
      },
      {
        id: 'a4',
        text: 'Officer witness to incident #4837 - Report submitted',
        subtext: 'Officer witness',
        time: '3 hours ago',
        type: 'incident'
      },
      {
        id: 'a5',
        text: 'STEP approval for Inmate #3032',
        subtext: 'Recommendation by Officer L',
        time: '3 hours ago',
        type: 'approval'
      },
      {
        id: 'a6',
        text: 'New inmate processed - Assigned to Block F Cell #8',
        subtext: 'Processed by Intake Officer',
        time: '5 hours ago',
        type: 'processing'
      }
    ];
  }

  onSearch(): void {
    const query = this.searchQuery.trim();

    if (!query) {
      this.alertService.showMessage(
        'Search',
        'Please enter a search term',
        MessageSeverity.warn
      );
      return;
    }

    this.alertService.showMessage(
      'Search',
      `Searching for: "${query}"`,
      MessageSeverity.info
    );

    // TODO: Implement actual search functionality
    console.log('Searching for:', query);
  }

  onQuickAction(action: QuickAction): void {
    this.alertService.showMessage(
      action.label,
      `Opening ${action.label}...`,
      MessageSeverity.info
    );

    // TODO: Implement navigation based on action.action
    console.log('Quick action:', action.action);
  }

  onInspectionClick(inspection: Inspection): void {
    this.alertService.showMessage(
      'Inspection Details',
      `${inspection.type} - ${inspection.block} ${inspection.cell}`,
      MessageSeverity.info
    );

    // TODO: Navigate to inspection details
    console.log('Inspection clicked:', inspection);
  }

  onActivityClick(activity: Activity): void {
    this.alertService.showMessage(
      'Activity Details',
      activity.text,
      MessageSeverity.info
    );

    // TODO: Show activity details modal/page
    console.log('Activity clicked:', activity);
  }

  viewAllInspections(): void {
    this.alertService.showMessage(
      'Inspections',
      'Opening all inspections view...',
      MessageSeverity.info
    );

    // TODO: Navigate to inspections page
  }

  viewAllActivity(): void {
    this.alertService.showMessage(
      'Activity Log',
      'Opening full activity log...',
      MessageSeverity.info
    );

    // TODO: Navigate to activity log page
  }

  refreshDashboard(): void {
    this.loadDashboardData();
  }

  getPriorityClass(priority: string): string {
    switch (priority) {
      case 'high':
        return 'text-danger';
      case 'medium':
        return 'text-warning';
      case 'low':
        return 'text-info';
      default:
        return '';
    }
  }

  getStatColorClass(index: number): string {
    const colors = ['stat-blue', 'stat-green', 'stat-yellow', 'stat-red'];
    return colors[index % colors.length];
  }

  private handleError(context: string, error: any): void {
    this.alertService.stopLoadingMessage();

    if (Utilities.checkNoNetwork(error)) {
      this.alertService.showStickyMessage(
        Utilities.noNetworkMessageCaption,
        Utilities.noNetworkMessageDetail,
        MessageSeverity.error,
        error
      );
      this.offerBackendDevServer();
    } else {
      const errorMessage = Utilities.getHttpResponseMessage(error);
      if (errorMessage) {
        this.alertService.showStickyMessage(
          context,
          errorMessage,
          MessageSeverity.error,
          error
        );
      } else {
        this.alertService.showStickyMessage(
          context,
          'An error occurred while processing your request.\nError: ' + Utilities.stringify(error),
          MessageSeverity.error,
          error
        );
      }
    }

    setTimeout(() => {
      this.isLoading = false;
    });
  }

  private offerBackendDevServer(): void {
    if (
      Utilities.checkIsLocalHost(location.origin) &&
      Utilities.checkIsLocalHost(this.configurations.baseUrl)
    ) {
      this.alertService.showDialog(
        'Dear Developer!<br />' +
        'It appears your backend Web API server is inaccessible or not running...<br />' +
        'Would you like to temporarily switch to the fallback development API server below? (Or specify another)',
        DialogType.prompt,
        (value) => {
          this.configurations.baseUrl = value as string;
          this.alertService.showStickyMessage(
            'API Changed!',
            'The target Web API has been changed to: ' + value,
            MessageSeverity.warn
          );
        },
        null,
        null,
        null,
        this.configurations.fallbackBaseUrl
      );
    }
  }



}
