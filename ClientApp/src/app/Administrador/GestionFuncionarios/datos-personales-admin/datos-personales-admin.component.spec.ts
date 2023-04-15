import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatosPersonalesAdminComponent } from './datos-personales-admin.component';

describe('DatosPersonalesAdminComponent', () => {
  let component: DatosPersonalesAdminComponent;
  let fixture: ComponentFixture<DatosPersonalesAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatosPersonalesAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatosPersonalesAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
