import { createRouter, createWebHistory } from "vue-router";

import { useUserStore } from "./stores/user";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "Dashboard",
            component: () => import("./views/Dashboard.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Management
        {
            path: "/users-management",
            name: "Users Management",
            component: () => import("./views/management/Users.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/donors-management",
            name: "Donors Management",
            component: () => import("./views/management/Donors.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/blood-management",
            name: "Blood Management",
            component: () => import("./views/management/Blood.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/events-management",
            name: "Events Management",
            component: () => import("./views/management/Events.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/hospitals-management",
            name: "Hospitals Management",
            component: () => import("./views/management/Hospitals.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Activity
        {
            path: "/donation-requests",
            name: "Donation Requests",
            component: () => import("./views/activity/DonationRequests.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/blood-requests",
            name: "Blood Requests",
            component: () => import("./views/activity/BloodRequests.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Detail
        {
            path: "/events/detail/:_id",
            name: "Event Detail",
            component: () => import("./views/detail/EventDetail.vue"),
            props: true,
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/donors/detail/:_id",
            name: "Donor Detail",
            component: () => import("./views/detail/DonorDetail.vue"),
            props: true,
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/hospitals/detail/:_id",
            name: "Hospital Detail",
            component: () => import("./views/detail/HospitalDetail.vue"),
            props: true,
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Form
        {
            path: "/event/new/",
            name: "Event Create",
            component: () => import("./views/form/EventForm.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/event/edit/:_id",
            name: "Event Edit",
            component: () => import("./views/form/EventForm.vue"),
            props: true,
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/hospital/new/",
            name: "Hospital Create",
            component: () => import("./views/form/HospitalForm.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        {
            path: "/hospital/edit/:_id",
            name: "Hospital Edit",
            component: () => import("./views/form/HospitalForm.vue"),
            props: true,
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Information
        {
            path: "/about",
            name: "About",
            component: () => import("./views/About.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
        // Login Page
        {
            path: "/login",
            name: "Login",
            component: () => import("./views/Login.vue"),
            meta: {
                layoutName: "LayoutUnauth",
                unguard: true,
            },
        },
        // Error Page
        {
            path: "/error/unauth",
            name: "Unauthorized Error",
            component: () => import("./views/error/UnauthError.vue"),
            meta: {
                layoutName: "LayoutUnauth",
                unguard: true,
            },
        },
        {
            path: "/error/server",
            name: "Server Error",
            component: () => import("./views/error/ServerError.vue"),
            meta: {
                layoutName: "__dynamic",
                unguard: true,
            },
        },
        {
            path: "/:pathMatch(.*)*",
            name: "404 Error",
            component: () => import("./views/error/404Error.vue"),
            meta: {
                layoutName: "__dynamic",
                unguard: true,
            },
        },
        // Hospital request page
        {
            path: "/hospital-request/:_id",
            name: "Hospital Page",
            component: () => import("./views/HospitalRequest.vue"),
            meta: {
                layoutName: "LayoutDefault",
            },
        },
    ],
    scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
            return savedPosition;
        } else {
            return { top: 0, behavior: "smooth" };
        }
    },
});

// Verify if user is logged in
router.beforeEach(async (to, from, next) => {
    const user = useUserStore();
    try {
        if (!user.isLoggedIn && !to.meta.unguard) {
            await user.verifyToken();
        }
        next();
    } catch (err) {
        if (!err.response) return next({ name: "Server Error" });
        if (err.response) {
            if (err.response.status === 401)
                return next({ name: "Unauthorized Error" });
            if (err.response.status === 500)
                return next({ name: "Server Error" });
        }

        throw err;
    }
});

// Prevent logged in user to access login page
router.beforeEach((to, from, next) => {
    if (to.name === "Login" && useUserStore().token) {
        return next({ name: from.name ? from.name : "Dashboard" });
    }
    next();
});

/* Changing the name of tab */
router.beforeEach((to, _, next) => {
    document.title = `${to.name} | Judoh Admin`;
    return next();
});

export default router;
