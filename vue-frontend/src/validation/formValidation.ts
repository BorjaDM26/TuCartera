import { extend } from 'vee-validate';
import {
  required,
  email,
  min,
  confirmed,
  min_value,
} from 'vee-validate/dist/rules';

extend('required', {
  ...required,
  message: 'Campo obligatorio',
});

extend('email', {
  ...email,
  message: 'Formato de email inválido',
});

extend('min', {
  ...min,
  validate: (value, { min }: any) => value.length >= min,
  params: ['min'],
  message: 'Requiere al menos {min} carácteres',
});

extend('min_value', {
  ...min_value,
  validate: (value, { min }: any) => value >= min,
  params: ['min'],
  message: 'El valor mínimo es {min}',
});

extend('confirmed', {
  ...confirmed,
  message: 'El valor no coincide',
});
