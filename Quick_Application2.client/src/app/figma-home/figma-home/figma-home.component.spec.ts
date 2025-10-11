import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { FigmaHomeComponent } from './figma-home.component';

describe('FigmaHomeComponent', () => {
  let component: FigmaHomeComponent;
  let fixture: ComponentFixture<FigmaHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FigmaHomeComponent,
        RouterTestingModule.withRoutes([]) // include if template uses routerLink
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(FigmaHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
