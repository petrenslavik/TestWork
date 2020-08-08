let testComponent = {
    template: '#test',
    data() {
        return {
            tests: []
        }
    },
    methods: {
        start(test) {
            this.$router.push({ path: '/test/start', query: { id: test.id } });
        }
    },
    created() {
        axios.get('/test/GetTests').then(response => { this.tests = response.data });
    }
};

let questionView = {
    template: '#question-view',
    data() {
        return {
            selected: "",
        }
    },
    props: {
        data: Object,
        index: Number,
        nextFunction: Function
    },
    methods: {
        save() {
            let selectedAnswer = this.data.answers.find(answer => answer.id === this.selected);
            this.nextFunction(selectedAnswer);
        }
    }
}

let results = {
    template: '#results',
    props: {
        selectedAnswers: Array,
        questions: Array
    },
    computed: {
        totalPoints() {
            let correct = 0;
            this.selectedAnswers.forEach(el => {
                if (el.isRight) {
                    correct+=1;
                }
            });
            return `${correct} out of ${this.selectedAnswers.length}`;
        }
    }
}

let testView = {
    template: '#test-view',
    data() {
        return {
            title: "",
            description: "",
            questions: [],
            agreed: false,
            started: false,
            currentQuestion: {},
            index: 0,
            selectedAnswers: [],
            finished: false
        }
    },
    methods: {
        start() {
            this.currentQuestion = this.questions[this.index++];
            this.started = true;
        },
        next(selectedAnswer) {
            this.selectedAnswers.push(selectedAnswer);
            if (this.index !== this.questions.length)
                this.currentQuestion = this.questions[this.index++];
            else
                this.finished = true;
        }
    },
    created() {
        var id = this.$route.query.id;
        axios.get(`/test/start?id=${id}`).then(response => {
            this.title = response.data.title;
            this.description = response.data.description;
            this.questions = response.data.questions;
        });
    },
    components: {
        'question-view': questionView,
        'results': results
    }
};

Vue.use(VueRouter);

let router = new VueRouter({
    routes: [
        { path: '/', component: testComponent },
        { path: '/test/start', component: testView }
    ]
});

let app = new Vue({
    el: '#app',
    router: router
})