<template>
  <div>
    <header class="header">
      <h1>SibersTask Project</h1>
    </header>

    <main>
      <div class="wizard-shell">
        <RouterView />
      </div>
    </main>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style scoped>
.header {
  background: var(--primary);
  color: var(--white);
  padding: 14px 0;
  text-align: center;
  font-size: 1.4rem;
  font-weight: 600;
  margin-bottom: 24px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
}
main {
  padding: 0 16px 40px;
}
</style>
