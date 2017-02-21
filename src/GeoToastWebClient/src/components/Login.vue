<template>
  <div>
    <div id="lock"></div>
  </div>
</template>

<script>
  import Auth0Lock from 'auth0-lock'

  export default {
    name: 'login',
    mounted () {
      const lock = new Auth0Lock('sAlZCm26CewF4re702Av7fMotT3KG027', 'jerrie.auth0.com', {
        container: 'lock'
      })

      lock.on('authenticated', (authResult) => {
        localStorage.setItem('id_token', authResult.idToken)
        this.$router.push('/')
      })

      lock.show()
    }
  }

</script>
