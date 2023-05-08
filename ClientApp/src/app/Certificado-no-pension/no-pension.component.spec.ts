import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoPensionComponent } from './no-pension.component';

describe('NoPensionComponent', () => {
  let component: NoPensionComponent;
  let fixture: ComponentFixture<NoPensionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoPensionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoPensionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
