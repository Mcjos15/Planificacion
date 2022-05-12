import React, { useState } from 'react'
import { Grid, Container, Paper, Avatar, Typography, TextField, Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import fondo from './assets/image/fondo.jpg'
import { LockOutlined as LockOutlinedIcon } from '@material-ui/icons'

//Objeto de estilos
const useStyle = makeStyles(theme => ({
  root: {
    backgroundImage: `url(${fondo})`,
    backgroundRepeat: 'no-repeat',
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    height: '100vh'
  },

  container: {
    opacity: '0.8',
    height: '60%',
    marginTop: theme.spacing(15),
    [theme.breakpoints.down(400 + theme.spacing(2) + 2)]: {
      marginTop: 0,
      width: '100%',
      height: '100%'
    }
  },

  div: {
    marginTop: theme.spacing(4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  },

  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.primary.main
  },

  form: {
    width: '100%',
    marginTop: theme.spacing(1)
  },

  button: {
    margin: theme.spacing(2, 0, 2)
  }
}))
//obtiene el password y el nickname
const App = () => {
  const [body, setBody] = useState({nickname:'', password:''})
  const classes = useStyle()

  const handleChange = (e) => {
       setBody({
         ...body, // tiene dentro todos los componentes de body
         [e.target.name]: e.target.value
       })
  }
//boton para mandar archivos
  const onSubmit =() => {
    console.log(body)

  }


  return (
    <Grid container component='main' className={classes.root} >
      <Container component={Paper} elevation={5} maxWidth='xs' className={classes.container} >
        <div className={classes.div}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component='h1' variant='h5'> Inicio de Sesión </Typography>
          <form className={classes.form}>
            <TextField
              fullWidth
              color='primary'
              margin='normal'
              variant='outlined'
              label='Usuario'
              name='nickname'
              value= {body.nickname}
              onChange= {handleChange}
              autoComplete= 'off'
              
            />

            <TextField
              fullWidth
              type='password'
              color='primary'
              margin='normal'
              variant='outlined'
              label='Contraseña'
              name='password'
              value= {body.password}
              onChange= {handleChange}
              autoComplete= 'off'
            />
            <Button
              fullWidth
              variant='contained'
              color='primary'
              className={classes.button}
              onClick= {()=>onSubmit()}
            >Iniciar Sesión</Button>
          </form>
        </div>
      </Container>
    </Grid>
  );
}

export default App