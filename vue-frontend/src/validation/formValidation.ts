import { extend } from 'vee-validate';
import { required, email, min, confirmed } from 'vee-validate/dist/rules';

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

extend('confirmed', {
  ...confirmed,
  message: 'El valor no coincide',
});
