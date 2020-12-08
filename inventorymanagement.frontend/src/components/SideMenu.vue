<template>
    <div class = "side-menu-container">
        <router-link to="/">
        <img id="imgLogo" alt="Inventory Mangement Logo" src="../assets/images/logo.png" />
        </router-link>
        <h1>Inventory Management Portal</h1>
        <inventory-button id="menuInventory" is-full-width @button:click="goToRoute('/inventory')">
            Inventory
        </inventory-button>
        <inventory-button id="menuCustomers" is-full-width @button:click="goToRoute('/customers')">
            Manage Customers
        </inventory-button>
         <inventory-button id="menuInvoice" is-full-width @button:click="goToRoute('/invoice/new')">
            New Invoice
        </inventory-button>
         <inventory-button id="menuOrders" is-full-width @button:click="goToRoute('/orders')">
            Orders
        </inventory-button>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import InventoryButton from "@/components/InventoryButton.vue";

    @Component({
        name: 'SideMenu',
        components: { InventoryButton }
    })

    export default class SideMenu extends Vue { 
        async goToRoute(route: string) {
            await this.$router.push(route);
        }
    }
</script>

<style scoped lang="scss">
    @import "@/scss/global.scss";

    .side-menu-container {
        background-color: #fcfcfc;
        height: 100vh;
        width: $menu-width;
        display: flex;
        flex-direction: column;
        padding: 0.8rem;
        box-shadow: 0 1px 1px rgba(0,0,0,0.12), 0 1px 1px rgba(0,0,0,0.24);
        box-sizing: border-box;
    }

    #imgLogo {
        width: 100%;
    }
    h1 {
        font-size: 1.2rem;
        margin: 1rem 0;
    }
</style>