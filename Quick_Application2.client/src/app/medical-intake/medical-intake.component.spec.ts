import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MedicalIntakeComponent } from './medical-intake.component';

describe('MedicalIntakeComponent', () => {
  let component: MedicalIntakeComponent;
  let fixture: ComponentFixture<MedicalIntakeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalIntakeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalIntakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
